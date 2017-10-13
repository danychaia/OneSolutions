package dev.edmt.ventasapp;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.GridView;
import android.widget.TextView;

import java.util.ArrayList;


/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link inventario.OnFragmentInteractionListener} interface
 * to handle interaction events.
 */
public class inventario extends Fragment {

    private OnFragmentInteractionListener mListener;

    public inventario() {
        // Required empty public constructor
    }


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment

        // Inflate the layout for this fragment
        ArrayList<String> arrayList = new ArrayList<>();
        arrayList.add("001");
        arrayList.add("Jugo");
        arrayList.add("100");
        arrayList.add("25");
        arrayList.add("75");
        arrayList.add("500");
        arrayList.add("L 5.2");
        arrayList.add("");
        arrayList.add("");
        arrayList.add("");
        arrayList.add("");
        arrayList.add("");
        arrayList.add("");
        arrayList.add("");
        arrayList.add("002");
        arrayList.add("Jabon");
        arrayList.add("100");
        arrayList.add("25");
        arrayList.add("75");
        arrayList.add("500");
        arrayList.add("L 5.2");
        // arrayList.add("Octavio");
        View rootView = inflater.inflate(R.layout.fragment_inventario , container, false);
        GridView grid = (GridView) rootView.findViewById(R.id.grdInventario );
        grid.setAdapter(new GridAdapterw(rootView.getContext(),arrayList));
        return rootView;
       // return inflater.inflate(R.layout.fragment_inventario, container, false);
    }

    // TODO: Rename method, update argument and hook method into UI event
    public void onButtonPressed(Uri uri) {
        if (mListener != null) {
            mListener.onFragmentInteraction(uri);
        }
    }

    @Override
    public void onAttach(Context context) {
        super.onAttach(context);
        if (context instanceof OnFragmentInteractionListener) {
            mListener = (OnFragmentInteractionListener) context;
        } else {
            throw new RuntimeException(context.toString()
                    + " must implement OnFragmentInteractionListener");
        }
    }

    @Override
    public void onDetach() {
        super.onDetach();
        mListener = null;
    }

    /**
     * This interface must be implemented by activities that contain this
     * fragment to allow an interaction in this fragment to be communicated
     * to the activity and potentially other fragments contained in that
     * activity.
     * <p>
     * See the Android Training lesson <a href=
     * "http://developer.android.com/training/basics/fragments/communicating.html"
     * >Communicating with Other Fragments</a> for more information.
     */
    public interface OnFragmentInteractionListener {
        // TODO: Update argument type and name
        void onFragmentInteraction(Uri uri);
    }
}

class GridAdapterw extends BaseAdapter
{
    Context context;
    ArrayList<String> arraylist;
    String texto="jkjajkjakfj";

    @Override
    public int getCount() {
        return arraylist.size();
    }

    public GridAdapterw(Context c,ArrayList<String> al_clientes)
    {
        context=c;
        arraylist = al_clientes;
    }

    @Override
    public Object getItem(int position) {
        return arraylist.get(position);
    }

    @Override
    public long getItemId(int position) {
        return 0;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        TextView textView = new TextView(context);
        textView.setText(String.valueOf(arraylist.get(position)));
        return textView;
    }
}


