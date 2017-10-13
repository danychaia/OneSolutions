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
 * {@link clientes.OnFragmentInteractionListener} interface
 * to handle interaction events.
 */
public class clientes extends Fragment {

    private OnFragmentInteractionListener mListener;

    public clientes() {
        // Required empty public constructor
    }


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        ArrayList<String> arrayList = new ArrayList<>();
        arrayList.add("CX01");
        arrayList.add("Abarroteria me Llega");
        arrayList.add("L 22,000");
        arrayList.add("1,36,55");
        arrayList.add("");
        arrayList.add("");
        arrayList.add("");
        arrayList.add("");
        arrayList.add("CX02");
        arrayList.add("Abarroteria Sirena");
        arrayList.add("L 25,000");
        arrayList.add("2,38");
       // arrayList.add("Octavio");
        View rootView = inflater.inflate(R.layout.fragment_clientes, container, false);
         GridView grid = (GridView) rootView.findViewById(R.id.grdClientes);
           grid.setAdapter(new GridAdapter(rootView.getContext(),arrayList));
        return rootView;

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

class GridAdapter extends BaseAdapter
{
    Context context;
    ArrayList<String> arraylist;
    String texto="jkjajkjakfj";
    public GridAdapter(Context c,ArrayList<String> al_clientes)
    {
        context=c;
        arraylist = al_clientes;
    }
    @Override
    public int getCount() {
        return arraylist.size() ;
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